export const MessageFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/messages/:id',
      {},
      {
        newMessage: {
          method: 'GET',
          url: 'api/messages/new'
        },
        newFile: {
          method: 'GET',
          url: 'api/messages/newFile'
        },
        archive: {
          method: 'POST',
          url: 'api/messages/:id/archive'
        },
        markAsRecieved: {
          method: 'POST',
          url: 'api/messages/:id/markAsRecieved'
        },
        sendMessage: {
          method: 'POST',
          url: 'api/messages/:id/send'
        },
        getCount: {
          method: 'GET',
          url: 'api/messagesCount'
        },
        getInboxMessages: {
          method: 'GET',
          url: 'api/inboxMessages'
        },
        getSentMessages: {
          method: 'GET',
          url: 'api/sentMessages'
        },
        getDraftMessages: {
          method: 'GET',
          url: 'api/draftMessages'
        },
        getArchivedMessages: {
          method: 'GET',
          url: 'api/archivedMessages'
        },
        getSentMessage: {
          method: 'GET',
          url: 'api/sentMessages/:id'
        },
        getIngoingMessage: {
          method: 'GET',
          url: 'api/ingoingMessages/:id'
        }
      }
    );
  }
];
