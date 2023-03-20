var ChatRoomLog = function(options) {

    var setupDisplayResult = function(options) {

        $('main').on('change',
            '#GranularityId',
            function () {

                function setData(content) {

                    $('main').find("#logContainer").html(content);
                };

                let granularityId = $(this).find(":selected").val();
                let roomId = $('main').find("#Id").val();

                $.ajax({
                    url: options.urlCheckLog,
                    data: {granularityId: granularityId, chatRoomId: roomId},
                    cache: false,
                    success: setData,
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                    }
                });
            });

    }

    function _init() {
        setupDisplayResult(options);
        
    }

    return {
        init: _init
    };
};