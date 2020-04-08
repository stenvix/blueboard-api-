using FluentMigrator.Runner.VersionTableInfo;

namespace BlueBoard.Persistence.Migrations
{
    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData
    {
        public object ApplicationContext { get; set; }

        public virtual string SchemaName { get; set; }

        public virtual string TableName => "scheme_versions";

        public virtual string ColumnName => "version";

        public virtual string UniqueIndexName => "unique_version";

        public virtual string AppliedOnColumnName => "applied_on";

        public virtual string DescriptionColumnName => "description";

        public virtual bool OwnsSchema => true;
    }
}
