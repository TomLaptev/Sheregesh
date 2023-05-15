mergeInto(LibraryManager.library, {
  RateGame: function () {
    ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);                        
                    }) 
            } else {
                console.log(reason);
                if (reason == "NO_AUTH") {
                  ysdk.auth.openAuthDialog();
                }
                
              }
              myGameInstance.SendMessage("GameManager", "PermissionRating");
        })  
  },

  SaveExtern: function (data) {
      var dataString = UTF8ToString(data);
      var myobj = JSON.parse(dataString);
      player.setData(myobj);
  },
  
  LoadExtern: function () {
       player.getData().then(_data => {
       const myJSON = JSON.stringify(_data); 
       myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON); 
       });
  },

  SetToLeaderboard: function (value) {
    ysdk.getLeaderboards()
  .then(lb => {
    lb.setLeaderboardScore('numberOfCoins', value);
 
  });
  },
 
  GetLang: function () {
   var lang = ysdk.environment.i18n.lang; 
   var bufferSize = lengthBytesUTF8(lang) + 1;
   var buffer = _malloc(bufferSize);
   stringToUTF8(lang, buffer, bufferSize);
   return buffer;
  },

  ShowAdv: function(){
    ysdk.adv.showFullscreenAdv({
      callbacks: {
          onClose: function(wasShown) {
            // some action after close
            console.log("============ closed =====");
            myGameInstance.SendMessage("GameManager", "AdvertisementShown");
          },
          onError: function(error) {
            // some action on error
          }
      }
  })  
  },

  ContinueForCoinsExtern: function () {
    ysdk.adv.showRewardedVideo({
      callbacks: {
          onOpen: () => {
            console.log('Video ad open.');
          },
          onRewarded: () => {
            console.log('Rewarded!');
           myGameInstance.SendMessage("GameManager", "Continue"); 
          },
          onClose: () => {
            console.log('Video ad closed.');
          }, 
          onError: (e) => {
            console.log('Error while open video ad:', e);
          }
      }
  })
   },

});
