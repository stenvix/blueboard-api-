using System;
using System.IO;
using FluentMigrator;

namespace BlueBoard.Persistence.Migrations
{
    [Migration(20200505, TransactionBehavior.Default, "Initial participants tables and scripts")]
    public class ParticipantMigration : Migration
    {
        private string baseDirectory;

        public ParticipantMigration()
        {
            this.baseDirectory = AppContext.BaseDirectory;
        }
        public override void Up()
        {
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Up/Structure/participant_v1.sql"));
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Up/Scripts/participant_v1.sql"));
        }

        public override void Down()
        {
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Down/Scripts/participant_v1.sql"));
            this.Execute.Script(Path.Combine(this.baseDirectory, "scripts/Down/Structure/participant_v1.sql"));
        }
    }
}
