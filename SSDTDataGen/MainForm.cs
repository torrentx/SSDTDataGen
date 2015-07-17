using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSDTDataGen
{
	public partial class MainForm : Form
	{
		#region Constants
		/// <summary>
		/// 0. [Schema].[Table]
		/// 1. List of Values in form (a,b,c),(a,b,c)
		/// 2. Source Table in form (A, B, C)
		/// 3. Join statement in form Source.Column = Target.Column
		/// 4. Update statement in form A = Source.A, B = Source.B
		/// </summary>
		public const string Import = ":r .\\{0}";
		public const string Merge = "MERGE INTO {0} AS Target ";
		public const string Using = "USING (VALUES ";
		public const string As = "AS Source {0}";
		public const string On = "ON {0}";
		public const string WhenMatched = @"
WHEN MATCHED THEN 
UPDATE SET 
{0}";
		public const string WhenNotMatched = @"
WHEN NOT MATCHED BY TARGET THEN 
INSERT {0}
VALUES {0}
";
		public const string Delete =@"
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;
GO";
		public const string IdentityInsertOn = @"
SET IDENTITY_INSERT {0} ON
GO";
		public const string IdentityInsertOff = @"
SET IDENTITY_INSERT {0} OFF
GO";
		public const string Column = @"[{0}]";

		public const string ConnectionStringTrusted = @"Data Source={0};Initial Catalog={1};Integrated Security=True;";
		public const string ConnectionStringUntrusted = @"Data Source={0};Initial Catalog={1};uid={2};pwd={3};";
		#endregion Constants

		public MainForm()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
			System.Data.DataTable dataTable = instance.GetDataSources();
			int count = dataTable.Rows.Count;
		}

		private void serverRefreshButton_Click(object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();
				DataTable databases = connection.GetSchema("Databases");
				foreach (DataRow database in databases.Rows)
				{
					String databaseName = database.Field<String>("database_name");
					databaseDDL.Items.Add(databaseName);
				}
			}
		}

		private void databaseDDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();
				DataTable schema = connection.GetSchema("Tables");
				List<string> TableNames = new List<string>();
				List<string> SchemaNames = new List<string>();
				foreach (DataRow row in schema.Rows)
				{
					if (!SchemaNames.Contains(row[1]))
					{
						SchemaNames.Add(row[1].ToString());
					}
					TableNames.Add(string.Format("[{0}].[{1}]", row[1], row[2]));
				}

				TableNames = (from tn in TableNames orderby tn select tn).ToList();
				SchemaNames = (from sn in SchemaNames orderby sn select sn).ToList();
				SchemaDDL.Items.AddRange(SchemaNames.ToArray());
				TableDDL.Items.AddRange(TableNames.ToArray());
			}
		}

		private void SchemaDDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();
				DataTable schema = connection.GetSchema("Tables");
				List<string> TableNames = new List<string>();
				foreach (DataRow row in schema.Rows)
				{
					if (row[1].Equals(SchemaDDL.SelectedItem))
					{
						TableNames.Add(string.Format("[{0}].[{1}]", row[1], row[2]));
					}
				}

				TableNames = (from tn in TableNames orderby tn select tn).ToList();
				TableDDL.Items.Clear();
				TableDDL.Items.AddRange(TableNames.ToArray());
			}
		}

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*";
			DialogResult dr = sfd.ShowDialog(this);
			if (!dr.Equals(DialogResult.OK))
			{
				return;
			}
			string path = Path.GetDirectoryName(sfd.FileName);
			string fileName = path + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName);
			string extension = Path.GetExtension(sfd.FileName).ToLower();


			string tableName = TableDDL.SelectedItem.ToString();
			List<string> columns = new List<string>();
			List<string> primaryKey = new List<string>();
			List<string> values = new List<string>();
			using (SqlConnection connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();
				DataColumnCollection c = GetPrimaryKeys(connection, tableName);

				foreach (DataColumn column in c)
				{
					// Only add non-calculated columns
					if (!column.ReadOnly || column.Unique)
					{
						columns.Add(string.Format(Column, column.ColumnName));
					}
				}

				foreach (DataColumn column in c[0].Table.PrimaryKey)
				{
					primaryKey.Add(string.Format(Column, column.ColumnName));
				}

				values = Values(connection, tableName);
			}

			if (SplitCheckBox.Checked)
			{
				WriteSplitFile(path, Path.GetFileNameWithoutExtension(sfd.FileName), extension, tableName, columns, primaryKey, values);
			}
			else
			{
				WriteFile(fileName + extension, tableName, columns, primaryKey, values);
			}
			MessageBox.Show("File Complete!");
		}

		private void WriteFile(string fileName, string tableName, List<string> columns, List<string> primaryKey, List<string> values)
		{
			using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					if (IdentityInsertCB.Checked)
					{
						sw.Write(string.Format(IdentityInsertOn, tableName));
						sw.Write(System.Environment.NewLine);
					}

					sw.Write(string.Format(Merge, tableName));
					sw.Write(Environment.NewLine);
					sw.Write(Using);
					sw.Write(Environment.NewLine);
					for (int i = 0; i < values.Count; ++i)
					{
						string value = values[i];
						sw.Write(value);
						if (i < values.Count - 1)
						{
							sw.Write(",");
							sw.Write(Environment.NewLine);
						}
					}
					sw.Write(")");
					sw.Write(Environment.NewLine);
					sw.Write(string.Format(As, GetSourceTable(columns)));
					sw.Write(Environment.NewLine);
					sw.Write(string.Format(On, GetJoinStatement(primaryKey)));
					sw.Write(string.Format(WhenMatched, GetUpdateStatement(columns, primaryKey)));
					sw.Write(string.Format(WhenNotMatched, GetSourceTable(columns)));
					sw.Write(Delete);

					if (IdentityInsertCB.Checked)
					{
						sw.Write(System.Environment.NewLine);
						sw.Write(string.Format(IdentityInsertOff, tableName));
					}
				}
			}
		}

		private void WriteSplitFile(string path, string baseFileName, string extension, string tableName, List<string> columns, List<string> primaryKey, List<string> values)
		{
			string fileName = path + "\\" + baseFileName;
			int i = 0;

			using (FileStream fs1 = new FileStream(fileName + "_" + i.ToString("D4") + extension, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				StreamWriter sw1 = new StreamWriter(fs1);

				if (IdentityInsertCB.Checked)
				{
					sw1.Write(string.Format(IdentityInsertOn, tableName));
					sw1.Write(System.Environment.NewLine);
				}

				List<string> subValues = values.Take(1000).ToList();
				++i;
				values = values.Skip(1000).ToList();
				WriteMerge(ref sw1, subValues, tableName, columns, primaryKey);
				sw1.Flush();
				sw1.Dispose();
			}


			while (values.Count > 0)
			{
				using (FileStream fs = new FileStream(fileName + "_" + i.ToString("D4") + extension, FileMode.OpenOrCreate, FileAccess.ReadWrite))
				{
					StreamWriter sw = new StreamWriter(fs);
					List<string> subValues = values.Take(1000).ToList();
					++i;
					values = values.Skip(1000).ToList();
					WriteMerge(ref sw, subValues, tableName, columns, primaryKey);
					sw.Flush();
					sw.Dispose();
				}
			}

			using (FileStream fs = new FileStream(fileName + "_" + i.ToString("D4") + extension, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					if (IdentityInsertCB.Checked)
					{
						sw.Write(System.Environment.NewLine);
						sw.Write(string.Format(IdentityInsertOff, tableName));
					}
				}
			}


			using (FileStream fs2 = new FileStream(fileName + extension, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				using (StreamWriter sw2 = new StreamWriter(fs2))
				{
					for (int j = 0; j <= i; ++j)
					{
						sw2.WriteLine(string.Format(Import, baseFileName + "_" + j.ToString("D4") + extension));
					}
				}
			}

		}

		private void WriteMerge(ref StreamWriter sw, List<string> values, string tableName, List<string> columns, List<string> primaryKey)
		{
			sw.Write(string.Format(Merge, tableName));
			sw.Write(Environment.NewLine);
			sw.Write(Using);
			sw.Write(Environment.NewLine);
			for (int i = 0; i < values.Count; ++i)
			{
				string value = values[i];
				sw.Write(value);
				if (i < values.Count - 1)
				{
					sw.Write(",");
					sw.Write(Environment.NewLine);
				}
			}
			sw.Write(")");
			sw.Write(Environment.NewLine);
			sw.Write(string.Format(As, GetSourceTable(columns)));
			sw.Write(Environment.NewLine);
			sw.Write(string.Format(On, GetJoinStatement(primaryKey)));
			sw.Write(string.Format(WhenMatched, GetUpdateStatement(columns, primaryKey)));
			sw.Write(string.Format(WhenNotMatched, GetSourceTable(columns)));
			sw.WriteLine(";");
			sw.WriteLine("GO");
		}

		private List<string> Values(SqlConnection connection, string tableName)
		{
			List<string> toRet = new List<string>();
			string statement = string.Empty;
			if (this.WhereTextBox.Text.Length > 0)
			{
				statement = "select * from " + tableName + " where " + WhereTextBox.Text;
			}
			else
			{
				statement = "select * from " + tableName;
			}
			using (SqlDataAdapter adapter = new SqlDataAdapter(statement, connection))
			using (DataTable table = new DataTable(tableName))
			{
				adapter.FillSchema(table, SchemaType.Source);
				adapter.Fill(table);


				foreach (DataRow dr in table.Rows)
				{
					StringBuilder row = new StringBuilder();
					row.Append("(");
					foreach (DataColumn dc in table.Columns)
					{
						if (dc.ReadOnly && !dc.Unique)
						{
							// Skip calculated columns
							continue;
						}
						else if (dr[dc] == System.DBNull.Value)
						{
							row.Append("NULL");
						}
						else if (
							(dr[dc].GetType() == Type.GetType("System.String")) ||
							(dr[dc].GetType() == Type.GetType("System.DateTime")) ||
							(dr[dc].GetType() == Type.GetType("System.Guid"))
							)
						{
							row.Append("'");
							row.Append(dr[dc].ToString());
							row.Append("'");
						}
						else if (dr[dc].GetType() == Type.GetType("System.Boolean"))
						{
							row.Append(((bool)dr[dc]) ? "1" : "0");
						}
						else
						{
							row.Append(dr[dc].ToString());
						}
						row.Append(",");
					}
					row.Remove(row.Length - 1, 1);
					row.Append(")");
					toRet.Add(row.ToString());
				}
			}
			return toRet;
		}

		private DataColumnCollection GetPrimaryKeys(SqlConnection connection, string tableName)
		{
			using (SqlDataAdapter adapter = new SqlDataAdapter("select * from " + tableName, connection))
			using (DataTable table = new DataTable(tableName))
			{
				return adapter.FillSchema(table, SchemaType.Source).Columns;
			}
		}

		private string GetSourceTable(List<string> columns)
		{
			StringBuilder SourceTable = new StringBuilder();
			SourceTable.Append("(");
			foreach (string column in columns)
			{
				SourceTable.Append(column);
				SourceTable.Append(",");
			}
			SourceTable.Remove(SourceTable.Length - 1, 1);
			SourceTable.Append(")");
			return SourceTable.ToString();
		}

		private string GetJoinStatement(List<string> keys)
		{
			StringBuilder JoinStatement = new StringBuilder();
			if (keys.Count > 0)
			{
				foreach (string column in keys)
				{
					JoinStatement.Append(string.Format("Source.{0} = Target.{0}", column));
					JoinStatement.Append(" AND ");
				}
				JoinStatement.Remove(JoinStatement.Length - 5, 5);
			}
			return JoinStatement.ToString();
		}

		private string GetUpdateStatement(List<string> columns, List<string> keys)
		{
			StringBuilder UpdateStatement = new StringBuilder();
			List<string> tempColumns = new List<string>();
			tempColumns.AddRange(columns);

			tempColumns.RemoveAll(m => keys.Contains(m));
			foreach (string column in tempColumns)
			{
				UpdateStatement.Append(string.Format("{0} = Source.{0}", column));
				UpdateStatement.Append(",");
			}
			if (UpdateStatement.Length > 0)
			{
				UpdateStatement.Remove(UpdateStatement.Length - 1, 1);
			}
			return UpdateStatement.ToString();
		}
		private string GetConnectionString()
		{
			if (SqlAuthCB.Checked)
			{
				return string.Format(ConnectionStringUntrusted, serverAddressTextBox.Text, databaseDDL.SelectedItem ?? "master", userNameTextBox.Text, PasswordTextBox.Text);
			}
			else
			{
				return string.Format(ConnectionStringTrusted, serverAddressTextBox.Text ?? "master", databaseDDL.SelectedItem);
			}
		}
	}
}