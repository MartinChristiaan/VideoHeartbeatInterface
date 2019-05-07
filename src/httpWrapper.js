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
    updateTarget:function(url,classnamefieldname,valuetype,value)
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
    }


} 
    

        
        
        

      