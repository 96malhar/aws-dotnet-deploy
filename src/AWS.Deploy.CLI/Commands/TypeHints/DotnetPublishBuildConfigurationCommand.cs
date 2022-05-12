// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading.Tasks;
using AWS.Deploy.Common;
using AWS.Deploy.Common.Recipes;
using AWS.Deploy.Common.TypeHintData;

namespace AWS.Deploy.CLI.Commands.TypeHints
{
    public class DotnetPublishBuildConfigurationCommand : ITypeHintCommand
    {
        private readonly IConsoleUtilities _consoleUtilities;
        private readonly IOptionSettingHandler _optionSettingHandler;

        public DotnetPublishBuildConfigurationCommand(IConsoleUtilities consoleUtilities, IOptionSettingHandler optionSettingHandler)
        {
            _consoleUtilities = consoleUtilities;
            _optionSettingHandler = optionSettingHandler;
        }

        public Task<List<TypeHintResource>?> GetResources(Recommendation recommendation, OptionSettingItem optionSetting) => Task.FromResult<List<TypeHintResource>?>(null);

        public Task<object> Execute(Recommendation recommendation, OptionSettingItem optionSetting)
        {
            var settingValue =
                _consoleUtilities.AskUserForValue(
                    string.Empty,
                    _optionSettingHandler.GetOptionSettingValue<string>(recommendation, optionSetting),
                    allowEmpty: false,
                    resetValue: _optionSettingHandler.GetOptionSettingDefaultValue<string>(recommendation, optionSetting) ?? "");
            recommendation.DeploymentBundle.DotnetPublishBuildConfiguration = settingValue;
            return Task.FromResult<object>(settingValue);
        }

        /// <summary>
        /// This method will be invoked to set the Dotnet build configuration in the deployment bundle
        /// when it is specified as part of the user provided configuration file.
        /// </summary>
        /// <param name="recommendation">The selected recommendation settings used for deployment <see cref="Recommendation"/></param>
        /// <param name="configuration">The user specified Dotnet build configuration.</param>
        public void Overridevalue(Recommendation recommendation, string configuration)
        {
            recommendation.DeploymentBundle.DotnetPublishBuildConfiguration = configuration;
        }
    }
}
