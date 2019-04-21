module.exports = {
    updateField:function(url,index,value)
    {       
        $.ajax({
            url: url,    //Your api url
            type: 'PUT',   //type is any HTTP method
            data: {
                value: value,
                index:index,                   
            },      //Data as js object
            success: function () {
            }
        })
        ;           
    }

} 
    

        
        
        

      