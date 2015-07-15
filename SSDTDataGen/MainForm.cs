using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Drawing;
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
		public const string ToFormat = @"
			MERGE INTO {0} AS Target 
			USING (VALUES {1}) 
			AS Source {2} 
			ON {3}
			--Update
			WHEN MATCHED THEN 
			UPDATE SET 
			{4}
			--Insert
			WHEN NOT MATCHED BY TARGET THEN 
			INSERT {2}
			VALUES {2}
			 --Delete
			WHEN NOT MATCHED BY SOURCE THEN 
			DELETE;
			GO";
		public const string IdentityInsertOn = @"
			SET IDENTITY_INSERT {0} ON
			GO";
		public const string IdentityInsertOff = @"
			SET IDENTITY_INSERT {0} OFF
			GO";

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

		private void TableDDL_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			string tableName = TableDDL.SelectedItem.ToString();
			List<string> columns = new List<string>();
			List<string> primaryKey = new List<string>();
			string values = string.Empty;
			using (SqlConnection connection = new SqlConnection(GetConnectionString()))
			{
				connection.Open();
				DataColumnCollection c = GetPrimaryKeys(connection, tableName);
				foreach (DataColumn column in c)
				{
					columns.Add(column.ColumnName);
				}
				foreach (DataColumn column in c[0].Table.PrimaryKey)
				{
					primaryKey.Add(column.ColumnName);
				}

				values = Values(connection, tableName);
			}

			StringBuilder fileBuilder = new StringBuilder();
			if (IdentityInsertCB.Checked)
			{
				fileBuilder.Append(string.Format(IdentityInsertOn, tableName));
				fileBuilder.Append(System.Environment.NewLine);
			}
			fileBuilder.Append(string.Format(ToFormat, tableName, values, GetSourceTable(columns), GetJoinStatement(primaryKey), GetUpdateStatement(columns, primaryKey)));
			if (IdentityInsertCB.Checked)
			{
				fileBuilder.Append(System.Environment.NewLine);
				fileBuilder.Append(string.Format(IdentityInsertOff, tableName));
			}
			MessageBox.Show(fileBuilder.ToString());
		}

		private string Values(SqlConnection connection, string tableName)
		{
			StringBuilder builder = new StringBuilder();
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
				adapter.Fill(table);

				foreach (DataRow dr in table.Rows)
				{
					builder.Append("(");
					foreach (DataColumn dc in table.Columns)
					{
						if (dr[dc] == System.DBNull.Value)
						{
							builder.Append("NULL");
						}
						else if (dr[dc].GetType() == Type.GetType("System.String"))
						{
							builder.Append("'");
							builder.Append(dr[dc].ToString());
							builder.Append("'");
						}
						else if (dr[dc].GetType() == Type.GetType("System.DateTime"))
						{
							builder.Append("'");
							builder.Append(dr[dc].ToString());
							builder.Append("'");
						}
						else if (dr[dc].GetType() == Type.GetType("System.Boolean"))
						{
							builder.Append(((bool)dr[dc])?"1":"0");
						}
						else
						{
							builder.Append(dr[dc].ToString());
						}
						builder.Append(",");
					}
					builder.Remove(builder.Length - 1, 1);
					builder.Append("),");
					builder.Append(Environment.NewLine);
				}

				if (table.Rows.Count > 0)
				{
					builder.Remove(builder.Length - 2, 2);
				}
			}
			return builder.ToString();
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
			columns.RemoveAll(m => keys.Contains(m));
			foreach (string column in columns)
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
			if ( SqlAuthCB.Checked )
			{
				return string.Format(ConnectionStringUntrusted, serverAddressTextBox.Text, databaseDDL.SelectedItem??"master", userNameTextBox.Text, PasswordTextBox.Text);
			}
			else
			{
				return string.Format(ConnectionStringTrusted, serverAddressTextBox.Text??"master", databaseDDL.SelectedItem);
			}
		}
	}
}