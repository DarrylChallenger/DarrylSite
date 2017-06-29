var quotes = [
"A horse, a horse, my kingdom for a horse",
"Et tu, Brute",
"My mistress' eyes are nothing like the sun",
"But at length the truth will out",
"Cry havoc and let slip the dogs of war",
"Jealousy is the green-eyed monster",
"Once more into the breach",
"To thine own self be true",
"Such stuff as dreams are made of",
"What the dickens",
"What's done is done and cannot be undone",
"Too much of a good thing",
"Method to my madness",
"Fight fire with fire",
"All's well that ends well"];

var s = document.getElementsByClassName("randomtext");
var r = Math.random() * quotes.length;
s[0].innerHTML = quotes[Math.floor(r)];

/*
$("document").ready(function() {
    $(".randomtext2").text(quotes[Math.floor(r)]);
});
*/

