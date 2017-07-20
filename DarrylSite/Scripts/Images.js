$(function() {
    $(".BlueEffect").hover(BlueFilter, BlueFilter);
    $(".BlueEffect2").hover(BlueFilter2, BlueFilter2);
    $(".OrigEffect").hover(OrigFilter, OrigFilter);
    $(".headshot").hover(headshot, headshot);
})
function BlueFilter() {
    $(".OrigEffect").toggleClass("OrigFilter")
}

function BlueFilter2() {
    $(".BlueEffect2").toggleClass("BlueFilter2a")
    $(".BlueEffect2").toggleClass("BlueFilter2b")
}

function OrigFilter() {
    $(".OrigEffect").toggleClass("OrigFilterA")
    $(".OrigEffect").toggleClass("OrigFilterB")
    $(".OrigLabel").toggleClass("OrigLabelA")
    $(".OrigLabel").toggleClass("OrigHide")
}

function headshot() {
    $(".headshot").toggleClass("opacity0")
    $(".headshot").toggleClass("opacity25")
}

