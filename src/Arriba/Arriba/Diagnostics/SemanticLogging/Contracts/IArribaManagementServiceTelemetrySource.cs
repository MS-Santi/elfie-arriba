using System;
using System.Collections.Generic;
using System.Text;

namespace Arriba.Diagnostics.SemanticLogging.Contracts
{
    public interface IArribaManagementServiceTelemetrySource
    {
        void NotifyUserTableColumnAdded();
        void NotifyUserTableCreated();
        void NotifyUserTableDeleted();
        void NotifyUserTableRowsDeleted();
        void NotifyDatabaseFetched();
        void NotifyUserTableInformationFetched();
        void NotifyTablesFetched();
        void NotifyUserTablesFetched();
        void NotifyTableAccessGranted();
        void NotifyAuthorizationPreconditionChecked();
        void NotifyUserTableReloaded();
        void NotifyTableAccessRevoked();
        void NotifyUserTableSaved();
        void NotifyAllUserTablesUnloaded();
        void NotifyUsertableUnloaded();
    }
}
