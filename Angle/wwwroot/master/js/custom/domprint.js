var index = [];


$("#selecttypes").chosen({
    no_results_text: "Oops, nothing found!",
    search_contains: true
});

$("#multiSelect").chosen({
    no_results_text: "Oops, nothing found!",
    search_contains: true
});



$("#TypeChild").chosen({
    no_results_text: "Oops, nothing found!",
    search_contains: true
});
$("#SelectedFeatures").chosen({
    no_results_text: "Oops, nothing found!",
    search_contains: true
});





function getTypeChilds(parents, parentID) {
    try {
        var childs = parents.find(x => x.ID == parentID)['Childs'];
        return childs;
    } catch{
        return 0;
    }
    
}



function clearSelectedTypes() {
    $('#selecttypes').html("");
    $("#selecttypes").chosen().trigger('chosen:updated');
    document.getElementById("listattributes").innerHTML = "";
   
}
//set childs in Edit Mode ONLOAD
function setMyChilds(childs) {
    var childsID = new Array();
    for (var i = 0; i < childs.length; i++) {
        var select = document.getElementById("selecttypes")
        var option = document.createElement("option");
        option.text = childs[i]['Description'];
        option.value = childs[i]['ID'];
        select.add(option);
        childsID.push(childs[i]['ID']);       
    }
    $('#selecttypes').val(childsID); 
    $("#selecttypes").chosen().trigger('chosen:updated');
   
}

function updateCloneMultiSelection() {
    $('#clonedchilds').val(myChilds);
    $("#clonedchilds").chosen().trigger('chosen:updated');
    $('#clonedfeatures').val(myFeatures);
    $("#clonedfeatures").chosen().trigger('chosen:updated');
    $('#multiSelect').val(myAttributes);
    $("#multiSelect").chosen().trigger('chosen:updated');
}


function setAvailbleChilds(selectID, parents, parentID) {
    var select = document.getElementById(selectID);
    var childs = getTypeChilds(parents, parentID);
    var avaibleChilds = new Array();
    if (childs != 0) {
        for (var i = 0; i < childs.length; i++) {

            avaibleChilds.push(myavaibles.filter(x => x.DeviceTypeID == childs[i]['Child']['ID']));
            // getAttributes(childs[i]['Child']['ID']);
        }

        for (var j = 0; j < avaibleChilds[0].length; j++) {
            var option = document.createElement("option");
            option.text = avaibleChilds[0][j]['SerialNumber'] + " " + avaibleChilds[0][j]['Description'];
            option.value = avaibleChilds[0][j]['ID'];
            select.add(option);
        }
    }
    $("#selecttypes").trigger("chosen:updated");
    console.log(avaibleChilds);
}


//allow duplicate selection of typechild
$('#TypeChild').on('change', function (e, params) {
    var selectedText = $("#TypeChild option:selected:last").html();
    if (params.selected != null) {
        $('#TypeChild').append('<option value=' + params.selected + '>' +selectedText + '</option>');
        $("#TypeChild").trigger("chosen:updated");
    }
    if (params.deselected != null) {
    }   
});
$('#multiSelect').on('change', function (e, params) {
    // alert(e.target.value);
    // alert(this.value);
    if (params.selected != null) {
        //setTypeChilds('selecttypes', parentsTypes, params.selected)
        createAttributeNode(params.selected);
        //getAttributes(params.selected);
    }

    if (params.deselected != null) {
        deleteNodes("attribute" + params.deselected, "listattributes");
        deleteNodes("hidden" + params.deselected, "hiddenAttributeID");
        // deleteAttribute(params.deselected);
    }
    //createNodes()
});


function cloneExistingAttributes() {
    var parentNode = document.getElementById("listattributes");
    var element = document.getElementById("cloneform").cloneNode(true);

}

//For Edit Page, it loads existing Product Attributes and Create Attributes Nodes;
function loadExistingAttributes() {
    var parentNode = document.getElementById("listattributes");
    // var attributes = child['Child']['TypeAttribute'];
    for (var i = 0; i < myAttributes.length; i++) {
        var element = document.getElementById("cloneform").cloneNode(true);
        var elementLabel = element.childNodes[1];
        var elemntInput = element.childNodes[3].firstChild.nextSibling;
        elementLabel.innerText = myAttributes[i]['Attribute']['Name'];
        createHiddenInputNode(myAttributes[i]['Attribute']['ID']);
        elemntInput.value = myAttributes[i]['Value'];
        elemntInput.setAttribute("name", "ValueSelectedAttributes");
        element.setAttribute("id", "attribute" + myAttributes[i]['Attribute']['ID']);
        parentNode.append(element);
    }
}


function createAttributeNode(id) {
    var parentNode = document.getElementById("listattributes");
    var name = document.getElementsByClassName("search-choice")[document.getElementsByClassName("search-choice").length - 1].childNodes[0].innerText;
    var element = document.getElementById("cloneform").cloneNode(true);
    var elementLabel = element.childNodes[1];
    var elemntInput = element.childNodes[3].firstChild.nextSibling;
    elementLabel.innerText = name; // selected text?
    var elemntInput = element.childNodes[3].firstChild.nextSibling;
    elemntInput.value = "";
    element.setAttribute("id", "attribute" + id);
    elemntInput.setAttribute("name", "ValueSelectedAttributes");
    parentNode.append(element);
    createHiddenInputNode(name, id);
}


function getAttributes(childID) {

    
    var child = parentsTypes.find(x => x.ID == childID)
   
    var separator = "<hr><div align='right' id='separator" + childID +"'>" + child['Name'] + "</div>";
    var parentNode = document.getElementById("listattributes");
    parentNode.innerHTML += separator;
    // var attributes = child['Child']['TypeAttribute'];
    for (var i = 0; i < child['TypeAttributes'].length; i++) {
        console.log(child['TypeAttributes'][i]);
        var element = document.getElementById("cloneform").cloneNode(true);
        var elementLabel = element.childNodes[1];
        var elemntInput = element.childNodes[3].firstChild.nextSibling;
        elementLabel.innerText = child['TypeAttributes'][i]['Attribute']['Name'];
        createHiddenInputNode(child['TypeAttributes'][i]['Attribute']['ID']);
        elemntInput.value = "";
        elemntInput.setAttribute("name", "ValueSelectedAttributes");
        element.setAttribute("id", "attribute" + child['TypeAttributes'][i]['Attribute']['ID']);
        parentNode.append(element);
    }
    var attributes = child['TypeAttributes'];
}

function deleteAttribute(childID) {
    var child = parentsTypes.find(x => x.ID == childID);
    var parent = document.getElementById("listattributes"); 
    
    for (var i = 0; i < child['TypeAttributes'].length; i++) {
        var childElement = document.getElementById("attribute" + child['TypeAttributes'][i]['Attribute']['ID']);
        parent.removeChild(childElement);
    }
    var child = document.getElementById("separator" + childID);
    var parent = document.getElementById("listattributes");
    parent.removeChild(child);
}


// VERAAAALTET BITTE LÖSCHEN!
function createNodes(item) {   
    var content = item.childNodes[0].innerText;
    createHiddenInputNode(content);
    if (document.getElementById("ipntxt" +
        content) == null) {
        var deleteButtom = item.childNodes[1];
        var cln = document.getElementById("cloneform").cloneNode(true);
        var formlabel = cln.childNodes[1];
        var forminput = cln.childNodes[3].firstChild.nextSibling;
        cln.setAttribute("id", "ipntxt" +
            content);
       // forminput.setAttribute("name", "ValueSelectedAttributes[" + deleteButtom.getAttribute("data-option-array-index")+"]");
        forminput.setAttribute("name", "ValueSelectedAttributes");
        forminput.value = "";
        deleteButtom.setAttribute("onclick", "deleteNodes('ipntxt" + content + "','" + content + "')")
        var test50 = deleteButtom.getAttribute("data-option-array-index");             
        formlabel.innerText = content + "*";
        var parentNode = document.getElementById("listattributes");
        parentNode.append(cln);
    }
}

function createHiddenInputNode(value, id) {
    var hiddenInput = document.createElement("input");
    hiddenInput.setAttribute("name", "SelectedAttributes");
    hiddenInput.setAttribute("value", value);
    hiddenInput.setAttribute("type", "hidden");
    hiddenInput.setAttribute("id", "hidden" + id);
    hiddenInput.setAttribute("class", "hiddenInputNodes");   
    document.getElementById("hiddenAttributeID").appendChild(hiddenInput);
}




function deleteNodes(id, parentnodeID) {
    var parent = document.getElementById(parentnodeID);
    var child = document.getElementById(id);
    parent.removeChild(child);   
}


function deleteHiddenInputNodes(value) {
    var hiddenInputNodes = document.getElementById("hiddenAttributeID");
    while (hiddenInputNodes.length)
        hiddenInputNodes.removeChild(document.getElementById("hidden" + value));
}




function nodeValues(content, value) {
    this.content = content;
    this.value = value;
}