using Arriba.Configuration;
using Arriba.Diagnostics.SemanticLogging.Contracts;
using Arriba.Model;
using Arriba.Model.Correctors;
using Arriba.ParametersCheckers;
using Arriba.Server.Authentication;

namespace Arriba.Communication.Server.Application
{
    public class ArribaManagementServiceFactory
    {
        private const string Table_People = "People";

        private readonly SecureDatabase secureDatabase;
        private readonly ClaimsAuthenticationService _claimsAuth;
        private readonly ISecurityConfiguration _securityConfiguration;
        private readonly IArribaManagementServiceTelemetrySource _telemetrySource;

        public ArribaManagementServiceFactory(SecureDatabase secureDatabase, ClaimsAuthenticationService claims, ISecurityConfiguration securityConfiguration, IArribaManagementServiceTelemetrySource telemetrySource)
        {
            ParamChecker.ThrowIfNull(secureDatabase, nameof(secureDatabase));
            ParamChecker.ThrowIfNull(claims, nameof(claims));
            ParamChecker.ThrowIfNull(securityConfiguration, nameof(securityConfiguration));

            this.secureDatabase = secureDatabase;
            _claimsAuth = claims;
            _securityConfiguration = securityConfiguration;
            _telemetrySource = telemetrySource;
        }

        public IArribaManagementService CreateArribaManagementService(string userAliasCorrectorTable = "")
        {
            if (string.IsNullOrWhiteSpace(userAliasCorrectorTable))
                userAliasCorrectorTable = Table_People;

            var correctors = new ComposedCorrector(new TodayCorrector(), new UserAliasCorrector(secureDatabase[userAliasCorrectorTable]));

            return new ArribaManagementService(secureDatabase, correctors, _claimsAuth, _securityConfiguration, _telemetrySource);
        }
    }
}