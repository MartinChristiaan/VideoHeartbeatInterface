module.exports = {
    getTargets:function(url,classnames,fieldnames,callback,wait)
    {       
        setTimeout(function(){
            $.ajax({
                url: url,    //Your api url
                type: 'PUT',   //type is any HTTP method
                data: {
                    classname: classnames,
                    fieldname:fieldnames
                                     
                },      //Data as js object
                success: function (result) {
                    callback(result)
                }
            })
            ; 
        }, wait);
                  
    },
    updateTarget:function(url,classname,fieldname,valuetype,value)
    {       
        $.ajax({
            url: url,    //Your api url
            type: 'PUT',   //type is any HTTP method
            data: {
                classname: classname,
                fieldname:fieldname,
                valuetype:valuetype,
                value:value                   
            },      //Data as js object
            success: function () {
                
            }
        })
        ;           
    },
    invokeMethod:function(url,classname,method)
    {       
        $.ajax({
            url: url,    //Your api url
            type: 'PUT',   //type is any HTTP method
            data: {
                classname: classname,
                method:method,
            },      //Data as js object
            success: function () {
                
            }
        })
        ;           
    },
    performRepeatedly:function(func,sleeptime)
    {
        function sleep(ms) {
            return new Promise(resolve => setTimeout(resolve, ms));
          }
          
          async function demo() {
            while(true)
            {
                await sleep(sleeptime);
                func();
            }
        }
          
          demo();
    },
    performAfter:function(func,sleeptime)
    {
        function sleep(ms) {
            return new Promise(resolve => setTimeout(resolve, ms));
          }
          
          async function demo() {
           
                await sleep(sleeptime);
                func();
           
        }
          
          demo();
    }



} 
    

        
        
        

      