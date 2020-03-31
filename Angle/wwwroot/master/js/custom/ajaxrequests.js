function addAttributeAjax() {
    $.post('/Attribute/Create', $('#attributeform').serialize(), function () {
        $('#attributesucces').html('Attribute was succesfuly added')
    });
    
}

function createAttribute() {
    $.post('/Attribute/CreateQuick', $('#attributeform').serialize(), function (data,status) {
        $('#attributesucces').html('Attribute was succesfuly added')
        return data;
    });

}