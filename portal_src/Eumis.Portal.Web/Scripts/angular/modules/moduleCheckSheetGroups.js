angular.module('moduleCheckSheetGroups', ['scaffolding', 'utils'])
    .controller('controllerCheckSheetGroups',
        ['$scope', '$filter', '$window', '$timeout', 'romanize',
            function ($scope, $filter, $window, $timeout, $romanize) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.groups = $window[globalKey][parentKey].model.CheckSheetGroupCollection;
                    $scope.users = $window[globalKey][parentKey].model.CheckSheetUserCollection;
                    $scope.userQuestions = $window[globalKey][parentKey].model.CheckSheetUserQuestionCollection;
                    $scope.userColonsCount = $scope.users.length;
                    $scope.showVerificationRow = $window[globalKey][parentKey].model.IsContractReportCheckSheet;

                    $scope.acceptances = $window[globalKey][parentKey].acceptances;

                    $scope.syncUserQuestions();

                    $scope.setUserColumnVisibility();
                };

                $scope.addNote = function (userQuestion) {
                    userQuestion.Note = '- ';
                };

                $scope.getNotesByUser = function (orderNum) {
                    return $scope.userQuestions.filter(function (userQuestion) {
                        return userQuestion.UserOrderNum === orderNum && userQuestion.Note;
                    });
                };

                $scope.assignAcceptance = function (userQuestion, acceptance) {
                    if (!userQuestion.Accept) {
                        userQuestion.Accept = {};
                    }

                    userQuestion.Accept.Name = acceptance.Name;
                };

                $scope.getUserQuestion = function (userOrderNum, groupOrderNum, questionOrderNum) {
                    var userQuestion = $scope.userQuestions.filter(function (userQuestion) {
                        return userQuestion.UserOrderNum === userOrderNum &&
                            userQuestion.GroupOrderNum === groupOrderNum &&
                            userQuestion.QuestionOrderNum === questionOrderNum;
                    });

                    return userQuestion[0];
                };

                $scope.syncUserQuestions = function () {
                    if (!$scope.userQuestions) {
                        return;
                    }

                    for (let k = 0; k < $scope.userQuestions.length; k++) {
                        $scope.userQuestions[k]['mapIndex'] = k;
                    }
                };

                $scope.setUserColumnVisibility = function () {
                    return $scope.users.filter(function (user) {
                        if (user.IsCurrentRespondent) {
                            user.ShowData = true;
                        }
                    });
                };

                $scope.showData = function (user) {
                    user.ShowData = true;
                };

                $scope.hideData = function (user) {
                    user.ShowData = false;
                };

                $scope.setNotesModalData = function (user, userQuestion) {
                    $scope.modalData = {
                        IsNoteModal: true,
                        Role : user.Role,
                        GroupOrderNum : userQuestion.GroupOrderNum,
                        QuestionOrderNum : userQuestion.QuestionOrderNum,
                        Text : userQuestion.Note
                    };
                };

                $scope.setSummaryModalData = function (user) {
                    $scope.modalData = {
                        IsNoteModal: false,
                        Role: user.Role,
                        Text: user.Summary
                    };
                };

                $scope.setApprovedPaymentTotal = function (user) {
                    user.VerificationData.ApprovedPaymentTotalAmount = Number(user.VerificationData.ApprovedPaymentEuAmount) + Number(user.VerificationData.ApprovedPaymentBgAmount);
                };

                $scope.getVerificationModalId = function (user, index) {
                    if (user.IsCurrentRespondent) {
                        return 'verificationModal_resp';
                    }

                    return 'verificationModal_' + index;
                };

                $scope.hideShowProcurementsTable = function () {
                    if ($scope.hideProcurementsTable) {
                        $scope.hideProcurementsTable = false;
                    }
                    else {
                        $scope.hideProcurementsTable = true;
                    }
                };

                $scope.romanize = function (num) {
                    return $romanize.convert(num);
                };

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
        }]
    );
