using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Cruds.WebViewUI.Migrations
{
    [Migration(031122021)]
    public class Migration_03122021 : FluentMigrator.Migration
    {
        public override void Down()
        {
            Delete.Table("Cliente");
        }

        public override void Up()
        {
            Create.Table("Cliente")
                .WithColumn("Codigo").AsInt32().PrimaryKey().NotNullable().Indexed().Identity(1,1)
                .WithColumn("Nome").AsString(50).Nullable()
                .WithColumn("Cpf").AsString(50).Nullable()
                .WithColumn("DataDeNascimento").AsDateTime().Nullable()
                .WithColumn("Email").AsString(50).Nullable()
                .WithColumn("RendaMensal").AsDecimal(18,0).Nullable();


            Insert.IntoTable("Cliente").Row(new { Nome = "John", Cpf = "030.231.231-12", DataDeNascimento = "05-02-1998", Email = "john@gmail.com", RendaMensal = "10"});
            Insert.IntoTable("Cliente").Row(new { Nome = "Ferd", Cpf = "456.234.234-45", DataDeNascimento = "12-05-1999", Email = "ferd@gmail.com", RendaMensal = "8"});
            Insert.IntoTable("Cliente").Row(new { Nome = "Gilson", Cpf = "342.533.543-43", DataDeNascimento = "25-06-1997", Email = "gilson@gmail.com", RendaMensal = "6"});
            


        }
    }
}
