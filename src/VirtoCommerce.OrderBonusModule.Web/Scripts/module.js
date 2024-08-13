// Call this to register your module to main application
var moduleName = 'OrderBonusModule';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.OrderBonusModuleState', {
                    url: '/OrderBonusModule',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'OrderBonusModule.helloWorldController',
                                template: 'Modules/$(VirtoCommerce.OrderBonusModule)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/OrderBonusModule',
                icon: 'fa fa-cube',
                title: 'OrderBonusModule',
                priority: 100,
                action: function () { $state.go('workspace.OrderBonusModuleState'); },
                permission: 'OrderBonusModule:access',
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
