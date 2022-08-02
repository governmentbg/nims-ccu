
function refreshProgrammeBudget() {
    $(".programme-budget-trigger").each(function (index, container) {
        triggerBroadcast(container, 'triggerBroadcast', {});
    });
}

function refreshProgramContract() {
    $(".program-contract-trigger").each(function (index, container) {
        triggerBroadcast(container, 'triggerBroadcast', {});
    });
}

function refreshProgrammeContractActivities() {
    $(".program-contract-activities").each(function (index, container) {
        triggerBroadcast(container, 'triggerBroadcast', {});
    });
}

function triggerBroadcast(container, broadcastName, args) {
    try {
        if ($(container).injector()) {
            $(container).injector().get('$rootScope').$broadcast(broadcastName, args);
        }
    } catch (err) {
        console.log(err);
    }
}
