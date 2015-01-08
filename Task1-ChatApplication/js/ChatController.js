(function () {
    angular.module("chatApp")
        .controller("ChatController", ChatController);
    ChatController.$inject = ["$scope", "$rootScope", "Hub"];

    function ChatController($scope, $rootScope, Hub) {
        var vm = this;

        vm.users = [];
        vm.chatList = null;
        vm.errorMessage = "";
        vm.userName = "";
        vm.isChatAvailable = false;
        vm.isUserLoggedIn = false;
        vm.userTypingMessage = "";

        var hub = new Hub('chatHub', {

            //client side methods
            listeners: {
                'showUsersOnLine': function (data) {
                    vm.users = data;
                    $rootScope.$apply();
                },
                'logInSuccess': function (data) {
                    console.log(data);
                    console.log(vm.userName);
                    if (vm.userName != data) {
                        vm.errorMessage = data;
                    } else {
                        vm.isChatAvailable = true;
                        vm.isUserLoggedIn = true;
                    }
                    $rootScope.$apply();
                },
                'logoutSuccess': function () {
                    vm.isChatAvailable = false;
                    vm.isUserLoggedIn = false;
                    $rootScope.$apply();
                },
                'broadcastMessage': function (data) {
                    vm.chatList = data;
                    vm.userTypingMessage = "";
                    vm.message = "";
                    $rootScope.$apply();
                },
                'checkUserTyping': function (data) {
                    vm.userTypingMessage = data;
                    $rootScope.$apply();
                }
            },

            methods: ['LogIn', 'Logout', 'Send', 'CheckUserTyping'],

            //queryParams: {
            //    'token': 'haseeb'
            //},

            errorHandler: function (error) {
                console.error(error);
            },

            //specify a non default root
            //rootPath: '/api

        });

        vm.signIn = signIn;
        vm.signout = signout;
        vm.sendMessage = sendMessage;
        vm.keyPress = keyPress;

        function signIn() {
            hub.LogIn(vm.userName);
        }

        function signout() {
            hub.Logout(vm.userName);
        }

        function sendMessage() {
            hub.Send(vm.userName, vm.message);
        }

        function keyPress() {
            hub.CheckUserTyping(vm.userName);
        }


    };
})();