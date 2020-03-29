using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Entities;
using FluentMigrator;

namespace BlueBoard.Persistence.Migrations
{
    [Migration(20200328, TransactionBehavior.Default, "Create user table")]
    public class CreateUserMigration : Migration
    {
        public override void Up()
        {
            this.Create.Table("users")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().Unique()
                .WithColumn("created").AsDateTime().NotNullable()
                .WithColumn("updated").AsDateTime().Nullable()
                .WithColumn("created_by").AsString(Constraints.EmailLength).NotNullable()
                .WithColumn("updated_by").AsString(Constraints.EmailLength).Nullable()
                .WithColumn("first_name").AsString(Constraints.NameLength).Nullable()
                .WithColumn("last_name").AsString(Constraints.NameLength).Nullable()
                .WithColumn("username").AsAnsiString(Constraints.NameLength).Nullable()
                .WithColumn("email").AsString(Constraints.EmailLength).NotNullable()
                .WithColumn("phone").AsString(Constraints.PhoneLength).Nullable()
                .WithColumn("status").AsByte().NotNullable();

            this.Execute.Script("Scripts/user_exists_v1.sql");
            this.Execute.Script("Scripts/create_user_v1.sql");
            this.Execute.Script("Scripts/find_user_by_email_v1.sql");
        }

        public override void Down()
        {
            this.Delete.Table("Users");
        }
    }
}
