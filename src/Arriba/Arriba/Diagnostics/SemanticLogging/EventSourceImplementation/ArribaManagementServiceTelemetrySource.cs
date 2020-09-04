using Arriba.Diagnostics.SemanticLogging.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace Arriba.Diagnostics.SemanticLogging.EventSourceImplementation
{
    public class ArribaManagementServiceTelemetrySource : EventSource, IArribaManagementServiceTelemetrySource
    {
        public ArribaManagementServiceTelemetrySource()
    : base(nameof(ArribaManagementServiceTelemetrySource))
        {
        }

        [Event(1)]
        public void NotifyAllUserTablesUnloaded()
        {
            WriteEvent(1, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(2)]
        public void NotifyAuthorizationPreconditionChecked()
        {
            WriteEvent(2, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(3)]
        public void NotifyDatabaseFetched()
        {
            WriteEvent(3, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(4)]
        public void NotifyTableAccessGranted()
        {
            WriteEvent(4, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(5)]
        public void NotifyTableAccessRevoked()
        {
            WriteEvent(5, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(6)]
        public void NotifyTablesFetched()
        {
            WriteEvent(6, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(7)]
        public void NotifyUserTableColumnAdded()
        {
            WriteEvent(7, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(8)]
        public void NotifyUserTableCreated()
        {
            WriteEvent(8, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(9)]
        public void NotifyUserTableDeleted()
        {
            WriteEvent(9, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(10)]
        public void NotifyUserTableInformationFetched()
        {
            WriteEvent(10, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(11)]
        public void NotifyUserTableReloaded()
        {
            WriteEvent(11, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(12)]
        public void NotifyUserTableRowsDeleted()
        {
            WriteEvent(12, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(13)]
        public void NotifyUserTableSaved()
        {
            WriteEvent(13, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(14)]
        public void NotifyUserTablesFetched()
        {
            WriteEvent(14, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        [Event(15)]
        public void NotifyUsertableUnloaded()
        {
            WriteEvent(15, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}
