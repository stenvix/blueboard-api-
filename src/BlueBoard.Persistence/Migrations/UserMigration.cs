using System;
using System.IO;
using FluentMigrator;

namespace BlueBoard.Persistence.Migrations
{
    [Migration(20200328, TransactionBehavior.Default, "Initial user tables and scripts")]
    public class UserMigration : Migration
    {
        private readonly string baseDirectory;

        public UserMigration()
        {
            this.baseDirectory = AppContext.BaseDirectory;
        }

        public override void Up()
        {
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Up/Structure/user_v1.sql"));
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Up/Scripts/user_v1.sql"));
        }

        public override void Down()
        {
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Down/Scripts/user_v1.sql"));
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Down/Structure/user_v1.sql"));
        }
    }
}
