using System;
using MapBuilder.Library.Models.Poco;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace MapBuilder.Library.Migration
{
    [Migration("1.0.0", 1, "NcMapBuilder")]
    public class MapBuilderMigration : MigrationBase
    {
        private readonly UmbracoDatabase _db = ApplicationContext.Current.DatabaseContext.Database;
        private readonly DatabaseSchemaHelper _sh;
        private readonly ILogger _logger;

        public MapBuilderMigration(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
            _logger = logger;
            _sh = new DatabaseSchemaHelper(_db, logger, sqlSyntax);
        }

        public override void Up()
        {
            try
            {
                _sh.CreateTable<NovicellMapBuilderMapsModel>(false);
                _sh.CreateTable<NovicellMapBuilderDataModel>(false);
            }
            catch (Exception e)
            {
                _logger.Error<MapBuilderMigration>("Error creating tables for NcMapBuilder", e);
            }
        }

        public override void Down()
        {
            try
            {
                _sh.DropTable<NovicellMapBuilderMapsModel>();
                _sh.DropTable<NovicellMapBuilderDataModel>();
            }
            catch (Exception e)
            {
                _logger.Error<MapBuilderMigration>("Error dropping tables for NcMapBuilder", e);
            }
        }
    }
}
