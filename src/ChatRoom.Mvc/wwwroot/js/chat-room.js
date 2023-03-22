var ChatRoom = function(options) {
    
    var setupChatActivity = function(options) {


        function sendMessage(postData) {
            $.ajax({
                method: 'POST',
                url: options.urlSendMessage,
                data: postData,
                cache: false,
                success: function (resultData, statusText, xhr) {
                    alert(xhr.responseText);
                },
                error: function (xhr, statusText, thrownError) {
                    alert(xhr.responseText);
                }
            });
        };

        $('button#sendHighFive').click(
            function(e) {
                e.preventDefault();

                let roomId = $('main').find("#Id").val();
                let participantId = $('main').find("#ParticipantId").val();
                let toParticipantId = $('div#chatActivity').find("#ToParticipantId").find(":selected").val();

                var postData = {
                    roomId: roomId,
                    participantId: participantId,
                    toParticipantId: toParticipantId
                };

                sendMessage(postData);
            });

        $('button#sendComment').click(
            function(e) {
                e.preventDefault();

                let roomId = $('main').find("#Id").val();
                let participantId = $('main').find("#ParticipantId").val();
                let message = $('div#chatActivity').find("#Message").val();
                if (message === '') {
                    alert("Please enter a comment.");
                    return;
                }
                

                var postData = {
                    roomId: roomId,
                    participantId: participantId,
                    message : message
                };

                sendMessage(postData);
            });
    }

    function _init() {
        setupChatActivity(options);
        
    }

    return {
        init: _init
    };
};