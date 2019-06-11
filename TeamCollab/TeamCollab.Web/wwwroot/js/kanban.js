var l1 = document.querySelector('#b1');
var l2 = document.querySelector('#b2');
var l3 = document.querySelector('#b3');

console.log([l1, l2, l3]);
console.log($("div.cards").toArray());


var drake = dragula($("div.cards").toArray());
// Scrollable area
var element = document.getElementById("boards"); // Count Boards
var numberOfBoards = element.getElementsByClassName('board').length;
var boardsWidth = numberOfBoards * 316; // Width of all Boards
element.style.width = boardsWidth + "px"; // set Width

// disable text-selection
function disableselect(e) { return false; }

// will implement a workaround later

//$(".cards").onselectstart = new Function();
//$(".cards").onmousedown = disableselect;

drake.on('drop',
    function (el, target, source, sibling) {
        if ($(target).parent().attr("id") === "bin") {
            return;
        }

        var listChildren = $(target).children();

        console.log(listChildren);
        var indexBefore = jQuery.inArray(el, listChildren) - 1;

        console.log("element: " + el.id.replace("card-", ""));

        var prevId = indexBefore < 0 ? null : listChildren[indexBefore].id.replace("card-", "");
        var nextId = sibling ? sibling.id.replace("card-", "") : null;

        console.log("prev: " + prevId);
        console.log("next: " + nextId);
        console.log("boardId: " + $(target).parent().attr("id").replace("board-", ""));

        moveCard(el.id.replace("card-", ""),
            $(target).parent().attr("id").replace("board-", ""),
            prevId,
            nextId);
    });

function moveCard(cardId, boardId, prevId, nextId) {
    $.ajax({
        method: "GET",
        url: `/Kanban/MoveCard?cardId=${cardId}&boardId=${boardId}&prevCardId=${prevId}&nextCardId=${nextId}`,
        success: function () {
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function addBoard() {
    $.ajax({
        method: "GET",
        url: `/Kanban/AddBoard?projectId=${$("#projectId").text()}&name=${$("#boardName").val()}`,
        success: function (board) {
            console.log(board);
            console.log('reload that shi...');
            location.reload();
        },
        error: function (err) {
            console.log(err);
            location.reload();
        }
    });
}

function addCard(id) {
    $.ajax({
        method: "GET",
        url: `/Kanban/AddCard?boardId=${id}&text=${$(`#cardName-${id}`).val()}&projectId=${$("#projectId").text()}`,
        success: function (board) {
            location.reload();
        },
        error: function (err) {
            console.log(err);
            location.reload();
        }
    });
}

$("div.board div input").on("keypress",
    function (event) {

        if (event.which === 13) {
            if (!event.shiftKey) {
                $(event.target).parent().find("button").click();

            }
            event.preventDefault();
        }
    });

$("#boardName").on("keypress",
    function (event) {
        if (event.which === 13) {
            if (!event.shiftKey) {
                $("#boardNameBtn").click();
            }
            event.preventDefault();
        }
    });

function archiveBoard(boardId) {
    $.ajax({
        method: "GET",
        url: `/Kanban/ArchiveBoard?boardId=${boardId}`,
        success: function () {
            location.reload();
        },
        error: function (err) {
            console.log(err);
            location.reload();
        }
    });
}

function archiveCards() {
    let cards = $("#recyclebin").children().toArray();

    if (cards.length == 0) {
        return;
    }

    console.log(cards);

    let index = 0;

    function recursiveForLoop() {
        $.ajax({
            method: "GET",
            url: `/Kanban/ArchiveCard?cardId=${$(cards[index]).attr("id").replace("card-", "")}`,
            success: function () {
                console.log(cards[index] + " archived!");
                index++;
                if (index < cards.length) {
                    recursiveForLoop();
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    recursiveForLoop();

    $("#recyclebin").empty();
}

$("#empty").on("click", archiveCards);

var height = $("ion-icon").css("height");
$(".details").css("height", height);