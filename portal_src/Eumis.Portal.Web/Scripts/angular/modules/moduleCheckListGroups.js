angular.module('moduleCheckListGroups', ['scaffolding', 'utils'])
    .controller('controllerCheckListGroups',
        ['$scope', '$filter', '$window', '$timeout', 'romanize',
            function ($scope, $filter, $window, $timeout, $romanize) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.groups = $window[globalKey][parentKey].model.CheckListGroupCollection;
                    $scope.users = $window[globalKey][parentKey].model.CheckListUserCollection;
                    $scope.userQuestions = $window[globalKey][parentKey].model.CheckListUserQuestionCollection;
                    $scope.hasGroups = $window[globalKey][parentKey].model.HasGroups;
                    $scope.hasUsers = $window[globalKey][parentKey].model.HasUsers;
                    $scope.userColonsCount = $scope.users.length;

                    $scope.isContractReportCheckList = $window[globalKey][parentKey].model.IsContractReportCheckList;

                    $scope.syncUserQuestions();

                    $scope.syncUserAnswersByGroup();
                };

                $scope.addGroup = function () {
                    let orderNum = 1;

                    if ($scope.groups.length > 0) {
                        orderNum = $scope.getNextOrderNum($scope.groups);
                    }

                    $scope.groups.push({
                        OrderNum: orderNum,
                        CheckListQuestionCollection: [],
                        IsNameValid: true,
                        HasQuestions: true
                    });
                };

                $scope.delGroup = function (index) {
                    var group = $scope.groups[index];

                    if (group.CheckListQuestionCollection.length > 0 && $scope.users.length > 0) {
                        for (var i = $scope.userQuestions.length - 1; i >= 0; i--) {
                            if ($scope.userQuestions[i].GroupOrderNum === group.OrderNum) {
                                $scope.userQuestions.splice(i, 1);
                            }
                        }

                        $scope.syncUserQuestions();
                    }

                    $scope.groups.splice(index, 1);

                    $scope.syncUserAnswersByGroup();
                };

                $scope.addQuestion = function (index) {
                    let questionOrderNum = 1;
                    let questions = $scope.groups[index].CheckListQuestionCollection;

                    if (questions.length > 0) {
                        questionOrderNum = $scope.getNextOrderNum(questions);
                    }

                    let groupOrderNum = $scope.groups[index].OrderNum;

                    if ($scope.users.length > 0) {
                        for (var i = 0; i < $scope.users.length; i++) {
                            $scope.userQuestions.push({
                                UserOrderNum: $scope.users[i].OrderNum,
                                GroupOrderNum: groupOrderNum,
                                QuestionOrderNum: questionOrderNum,
                                IsApplicable: false
                            });
                        }

                        $scope.syncUserQuestions();
                    }

                    $scope.groups[index].CheckListQuestionCollection.push({
                        OrderNum: questionOrderNum,
                        IsContentValid: true
                    });

                    $scope.syncUserAnswersByGroup();
                };

                $scope.delQuestion = function (index1, index2) {
                    let groupOrderNum = $scope.groups[index1].OrderNum;
                    let questionOrderNum = $scope.groups[index1].CheckListQuestionCollection[index2].OrderNum;

                    if ($scope.users.length > 0) {
                        for (var i = $scope.userQuestions.length - 1; i >= 0; i--) {
                            if ($scope.userQuestions[i].GroupOrderNum === groupOrderNum &&
                                $scope.userQuestions[i].QuestionOrderNum === questionOrderNum) {
                                $scope.userQuestions.splice(i, 1);
                            }
                        }

                        $scope.syncUserQuestions();
                    }

                    $scope.groups[index1]
                        .CheckListQuestionCollection.splice(index2, 1);
                };

                $scope.addUser = function () {
                    let orderNum = 1;

                    if ($scope.users.length > 0) {
                        orderNum = $scope.getNextOrderNum($scope.users);
                    }

                    let newUser = {
                        OrderNum: orderNum,
                        IsRoleValid: true
                    };

                    if ($scope.isContractReportCheckList) {
                        newUser.HideVerificationData = false;
                    }

                    for (var i = 0; i < $scope.groups.length; i++) {
                        for (var j = 0; j < $scope.groups[i].CheckListQuestionCollection.length; j++) {
                            let groupOrderNum = $scope.groups[i].OrderNum;
                            let questionOrderNum = $scope.groups[i].CheckListQuestionCollection[j].OrderNum;

                            $scope.userQuestions.push({
                                UserOrderNum: newUser.OrderNum,
                                GroupOrderNum: groupOrderNum,
                                QuestionOrderNum: questionOrderNum,
                                IsApplicable: false
                            });
                        }
                    }

                    $scope.syncUserQuestions();

                    $scope.users.push(newUser);

                    $scope.userColonsCount = $scope.userColonsCount + 1;
                };

                $scope.delUser = function (index) {
                    let userOrderNum = this.users[index].OrderNum;

                    for (var i = $scope.userQuestions.length - 1; i >= 0; i--) {
                        if ($scope.userQuestions[i].UserOrderNum === userOrderNum) {
                            $scope.userQuestions.splice(i, 1);
                        }
                    }

                    $scope.syncUserQuestions();

                    $scope.users.splice(index, 1);

                    $scope.syncUserAnswersByGroup();

                    $scope.userColonsCount = $scope.userColonsCount - 1;
                };

                $scope.getUserQuestion = function (userOrderNum, groupOrderNum, questionOrderNum) {
                    var userQuestion = $scope.userQuestions.filter(function (userQuestion) {
                        return userQuestion.UserOrderNum === userOrderNum &&
                            userQuestion.GroupOrderNum === groupOrderNum &&
                            userQuestion.QuestionOrderNum === questionOrderNum;
                    });

                    return userQuestion[0];
                };

                $scope.setUserQuestion = function (userQuestionInput) {
                    var userQuestion = $scope.getUserQuestion(userQuestionInput.UserOrderNum,
                        userQuestionInput.GroupOrderNum,
                        userQuestionInput.QuestionOrderNum);

                    userQuestion.IsApplicable = userQuestionInput.IsApplicable;

                    $scope.syncUserAnswersByGroup();
                };


                $scope.getNextOrderNum = function (array) {
                    let newArray = JSON.parse(JSON.stringify(array));

                    newArray.sort(function (a, b) {
                        return b.OrderNum - a.OrderNum;
                    });

                    return Number(newArray[0].OrderNum) + 1;
                };

                $scope.syncUserQuestions = function () {
                    if (!$scope.userQuestions) {
                        return;
                    }

                    for (let k = 0; k < $scope.userQuestions.length; k++) {
                        $scope.userQuestions[k]['mapIndex'] = k;
                    }
                };

                $scope.syncUserAnswersByGroup = function () {
                    for (let i = 0; i < $scope.groups.length; i++) {
                        let group = $scope.groups[i];
                        group.groupUsers = [];

                        for (let j = 0; j < $scope.users.length; j++) {
                            let user = $scope.users[j];
                            let userQuestions = $scope.userQuestions.filter(function (item) {
                                return item.GroupOrderNum === group.OrderNum && item.UserOrderNum === user.OrderNum && item.IsApplicable === false;
                            });

                            if (userQuestions.length === 0) {
                                group.groupUsers[j] = true;
                            }
                        }
                    }
                };

                $scope.setUserAnswersForGroup = function (groupOrderNum, userOrderNum, acceptAll) {
                    let userQuestions = $scope.userQuestions.filter(function (item) {
                        return item.GroupOrderNum === groupOrderNum && item.UserOrderNum === userOrderNum;
                    });

                    for (var i = 0; i < userQuestions.length; i++) {
                        userQuestions[i].IsApplicable = acceptAll;
                    }
                };

                $scope.romanize = function (num) {
                    return $romanize.convert(num);
                };

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
        }]);
