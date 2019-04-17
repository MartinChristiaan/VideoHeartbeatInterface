module.exports = {
    getRequest:function(url)
    { 
        const Http = new XMLHttpRequest();
        //const url='http://127.0.0.1:5000/timestep';
        
        Http.open("GET", url);
        Http.setRequestHeader("Access-Control-Allow-Origin","http://127.0.0.1:5000/timestep")
        
        Http.send();
        Http.onreadystatechange=(e)=>{}

        return Http.responseText
    },
    putRequest:function(url,sendData)
    {       
        
            
 
                $.ajax({
                    url: url,    //Your api url
                    type: 'PUT',   //type is any HTTP method
                    data: {
                        data: sendData
                    },      //Data as js object
                    success: function () {
                    }
                })
                ;

            
    }
} 
    

        
        
        

      