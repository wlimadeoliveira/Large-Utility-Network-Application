function setExistingAttributes(myAttributes) {
    var parentNode = document.getElementById("listattributes");
    // var attributes = child['Child']['TypeAttribute'];
    for (var i = 0; i < myAttributes.length; i++) {
        var element = document.getElementById("cloneform").cloneNode(true);
        var elementLabel = element.childNodes[1];
        var elemntInput = element.childNodes[3].firstChild.nextSibling;
        elementLabel.innerText = myAttributes[i]['Attribute']['Name'];
        CreateHiddenInputNode(myAttributes[i]['Attribute']['Name'],myAttributes[i]['AttributeID']);
        elemntInput.value = myAttributes[i]['Value'];
        elemntInput.setAttribute("name", "ValueSelectedAttributes");
        element.setAttribute("id", "attribute" + myAttributes[i]['Attribute']['ID']);
        parentNode.append(element);
    }
}

//Allow multiple selection on typechild in type edit view
$('#mychilds').on('change', function (e, params) {
    var selectedText =  $("#mychilds option:selected:last").html();

    if (params.selected != null) {
        $('#mychilds').append('<option value=' + params.selected + '>' + selectedText + '</option>');
        $("#mychilds").trigger("chosen:updated");
    }
    if (params.deselected != null) {

    }
});


$('#editmultiSelect').on('change', function (e, params) {
    // alert(e.target.value);
    // alert(this.value);
    if (params.selected != null) {
        //setTypeChilds('selecttypes', parentsTypes, params.selected)
        editCreateAttributeNode(params.selected);
        //getAttributes(params.selected);
    }

    if (params.deselected != null) {
        DeleteNodes("attribute" + params.deselected, "listattributes");
        DeleteNodes("hidden" + params.deselected, "hiddenAttributeID");
        // deleteAttribute(params.deselected);
    }
    //createNodes()
});

//For Edit Type
function editCreateAttributeNode(id) {
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
    editCreateHiddenInputNode(name, id);
}

function CreateHiddenInputNode(name, id) {
    var hiddenInput = document.createElement("input");
    hiddenInput.setAttribute("name", "SelectedAttributes");
    hiddenInput.setAttribute("value", name);
    hiddenInput.setAttribute("type", "hidden");
    hiddenInput.setAttribute("id", "hidden" + id);
    hiddenInput.setAttribute("class", "hiddenInputNodes");
    document.getElementById("hiddenAttributeID").appendChild(hiddenInput);
}

//FOR TYPE EDIT MOD!
function editCreateHiddenInputNode(value, id) {
    var hiddenInput = document.createElement("input");
    hiddenInput.setAttribute("name", "SelectedAttributes");
    hiddenInput.setAttribute("value", id);
    hiddenInput.setAttribute("type", "hidden");
    hiddenInput.setAttribute("id", "hidden" + id);
    hiddenInput.setAttribute("class", "hiddenInputNodes");
    document.getElementById("hiddenAttributeID").appendChild(hiddenInput);
}

function DeleteNodes(id, parentnodeID) {
    var parent = document.getElementById(parentnodeID);
    var child = document.getElementById(id);
    parent.removeChild(child);
}
