var initClock = function () {

    var __ticks = Date.now();

    var _tickInterval = 250;
    var _refreshInterval = 60000;

    var updateTime = function (date) {
        $("#sec").html((date.getSeconds() < 10 ? "0" : "") + date.getSeconds());
        $("#min").html((date.getMinutes() < 10 ? "0" : "") + date.getMinutes());
        $("#hours").html((date.getHours() < 10 ? "0" : "") + date.getHours());
    };

    var getServerTime = function() {
        $.ajax({
            type: 'POST',
            url: '/api/time/now',
            data: '{ }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function(ticks) {
                if (ticks != null) {
                    __ticks = ticks;
                }
            }
        });
    };
    
    setInterval(function () {
        getServerTime();
    }, _refreshInterval);

    setInterval(function () {
        __ticks = __ticks + _tickInterval;

        updateTime(new Date(__ticks));
    }, _tickInterval);

    updateTime(new Date(__ticks));
    getServerTime();
};
