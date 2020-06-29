var cc = DataStudioApp.createCommunityConnector();


var host = "...gateway endpoint goes here..."; 

function isAdminUser(){
  return true;
}

// https://developers.google.com/datastudio/connector/reference#getauthtype
 function getAuthType() {
  var cc = DataStudioApp.createCommunityConnector();
  return cc.newAuthTypeResponse()
    .setAuthType(cc.AuthType.KEY)
    .build();
}

function setCredentials(request){
  
  var key = request.key;

   var options = {
      'method' : 'post',
      'contentType': 'application/json',
      // Convert the JavaScript object to a JSON string.
      'payload' : JSON.stringify(request)
    };

  var validKey =  UrlFetchApp.fetch(host + "/api/v202004/Auth?AuthActionType=SetCredentials&key=" + key, options);
  var userProperties = PropertiesService.getUserProperties();
  userProperties.setProperty('dscc.key', key);
  
  return {
    errorCode: 'NONE'
  };
  if (!validKey) {
    return {
      errorCode: 'INVALID_CREDENTIALS'
    };
  }
  //var userProperties = PropertiesService.getUserProperties();
  //var key2 = userProperties.getProperty('dscc.key');
  
  return {
    errorCode: 'NONE'
  };
} 

function isAuthValid(p){
  
  var userProperties = PropertiesService.getUserProperties();
  var key = userProperties.getProperty('dscc.key');
  // This assumes you have a validateKey function that can validate
  // if the key is valid.
  var kobj = { 'key':key, 'request':p};
   var options = {
      'method' : 'post',
      'contentType': 'application/json',
      // Convert the JavaScript object to a JSON string.
     'payload' : JSON.stringify(kobj) ,
    };

  var resp =  UrlFetchApp.fetch(host + "/api/v202004/Auth?AuthActionType=IsAuthValid&key=" + key, options);
    var respTxt = resp. getContentText();  
  var respVal = (respTxt == "true");
  return respVal;
}

function resetAuth(p){
  var userProperties = PropertiesService.getUserProperties();
  userProperties.deleteProperty('dscc.key');
  
   var options = {
      'method' : 'post',
      'contentType': 'application/json',
      // Convert the JavaScript object to a JSON string.
      'payload' : JSON.stringify(p)
    };

  var resp =  UrlFetchApp.fetch(host + "/api/v202004/Auth?AuthActionType=ResetAuth&key=" + key, options);
  return resp;
}
 

function getConfig(request) {
  var userProperties = PropertiesService.getUserProperties();
  var key = userProperties.getProperty('dscc.key');

  var options = {
    'method' : 'post',
    'contentType': 'application/json',
    // Convert the JavaScript object to a JSON string.
    'payload' : JSON.stringify(request)
  };
  var resp =  UrlFetchApp.fetch(host + "/api/v202004/Config?key=" + key, options);
  
  var respTxt = resp. getContentText();
  var respObj = JSON.parse(respTxt);
  //return {schema: getFields().build()};
  
  return respObj;
}


// https://developers.google.com/datastudio/connector/reference#getschema
function getSchema(request) {
  var userProperties = PropertiesService.getUserProperties();
  var key = userProperties.getProperty('dscc.key');

  var options = {
    'method' : 'post',
    'contentType': 'application/json',
    // Convert the JavaScript object to a JSON string.
    'payload' : JSON.stringify(request)
  };
  var resp =  UrlFetchApp.fetch(host + "/api/v202004/Schema?key=" + key, options);
  //
  //return {schema: getFields().build()};
  var respTxt = resp.getContentText();
  var respObj = JSON.parse(respTxt);
  
  return respObj;
}



function getData(request){
  var userProperties = PropertiesService.getUserProperties();
  var key = userProperties.getProperty('dscc.key');
  
    var options = {
      'method' : 'post',
      'contentType': 'application/json',
      // Convert the JavaScript object to a JSON string.
      'payload' : JSON.stringify(request)
    };

  var resp =  UrlFetchApp.fetch(host + "/api/v202004/Data?key=" + key, options);
  var respTxt = resp.getContentText();
  var respObj = JSON.parse(respTxt);
  
  return respObj;
}
