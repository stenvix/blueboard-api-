using System;
using System.IO;
using FluentMigrator;

namespace BlueBoard.Persistence.Migrations
{
    [Migration(20200408, TransactionBehavior.Default, "Initial trip tables and scripts")]
    public class TripMigration : Migration
    {
        private readonly string baseDirectory;

        public TripMigration()
        {
            this.baseDirectory = AppContext.BaseDirectory;
        }

        public override void Up()
        {
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Up/Structure/trip_v1.sql"));
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Up/Scripts/trip_v1.sql"));
        }

        public override void Down()
        {
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Down/Scripts/trip_v1.sql"));
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Down/Structure/trip_v1.sql"));
        }
    }
}
