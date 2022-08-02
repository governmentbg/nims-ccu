var initClock = function () {

    var __ticks = Date.now();

    var _tickInterval = 250;
    var _refreshInterval = 60000;

    var updateTime = function (date) {
        // $("#sec").html((date.getSeconds() < 10 ? "0" : "") + date.getSeconds());
        $("#min").html((date.getMinutes() < 10 ? "0" : "") + date.getUTCMinutes());
        $("#hours").html((date.getHours() < 10 ? "0" : "") + date.getHours());
    };

    var getServerTime = function () {
        var route = window['_eumis_options']['session'];
        $.ajax({
            type: 'POST',
            url: '/api/' + route + '/time/now',
            data: '{ }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function(time) {
                if (time != null) {
                    __ticks = (new Date(time.year, time.month, time.day, time.hour, time.minute, time.second)).getTime();
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
