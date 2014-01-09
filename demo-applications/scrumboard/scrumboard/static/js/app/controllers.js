var AppRouter = Backbone.Router.extend({
    routes : {
        "" : "list",
        "board/:id" : "board"
    },
    list : function () {
        this.boards = new BoardSet();
        this.boards_view = new BoardSetView({model:this.boards})
        this.boards.fetch(); // retrieve from server
    },
    board : function (id) {
        var self = this;
        var _select_board = function () {
            $("#boarditem-" + id).addClass("active").siblings().removeClass("active")
        }
        if (!this.boards_view) this.list();
        this.boards.on("reset", _select_board);
        _select_board();
        var stages = new StageSet();
        this.stageset_view = new StageSetView({model:stages})
        stages.fetch({data:{board:id}})
        this.active_board = new Board({id : id})
        this.board_view = new BoardView({model:this.active_board})
        this.active_board.fetch()

    }
})