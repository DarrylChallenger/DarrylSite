$(function () { 
    $("<h3>prependTo</h3>").prependTo("#centerpoint2");
    $("<h2>insertBefore</h2>").insertBefore("#centerpoint2");
    $("<h3>appendTo</h3>").appendTo("#centerpoint2"); 
    $("<h2>insertAfter</h2>").insertAfter("#centerpoint2");
    $("#centerpoint2").attr("test", "result");
});


function addContentBefore() {

}

function addContentAfter() {

}

function addContent() {
    console.log("In addContent()")
    var target = $("#centerpoint") //.attr("id", "aftercp") //     <div id="centerpoint">Center Point</div>

    var target2 = $(".otherpoint").attr("id", "otherpt")

    //target.insertAfter("<div>insertAfter</div>")
    //$("<p>Test</p>").insertBefore("#centerpoint")
    /*
    target.after("<span>afterSpan</span>")
    target.append("<div>append (on next line)<div>embedded")
    
    target.prepend("<div>prependX<div>Embedded")
    target.before("<div>before</div>").before("<span>before span</span>")

    var s = $("#container")
    $("#outwin").html(s)
    console.log(s)
    */
    var jqDOMText = '<div id ="jqDOMText" class="w3-panel w3-note"> \
        < p > <b>DOM = Document Object Model</b> <br><br> \
            The DOM defines a standard for accessing HTML and XML documents:<br><br> \
                <i>"The W3C Document Object Model (DOM) is a platform and language-neutral \
interface that allows programs and scripts to dynamically access and update the \
content, structure, and style of a document."</i></p> \
</div>'
    target2.append("<form id='newForm'>child1").append("<div>child2").after("<div>sib1").append("<div>child3").prepend("<div>child0").after("<div>sib2").before("sib0")
    var y = $("#mycontainer:contains('QWERTY')")
    console.log(y.html())
    console.log(y)
    y.after("after-(#mycontainer:contains('QWERTY'))")
    y.append("append-(#mycontainer:contains('QWERTY'))")
    $("input[name*='man']").val("has man in it!");
    var copy = $("#formWithButtonsonRight").clone()
    console.log(copy.html())
    $("#newForm").append(copy)


}