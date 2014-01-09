var StoryView = Backbone.View.extend({
    tagName : "li",
    template : _.template( $("#StoryTemplate").html()),
    events : {
        'click a' : 'edit_story'
    },
    edit_story : function () {
        $("#story-form").html(_.template($("#StoryForm").html()));
        $("#story-form").modal("show");

        // populate fields
        $("#story-form #description").val(this.model.get("description"));
        $("#story-form #color").val(this.model.get("color"));
        $("#story-form").find("h2").html("Edit Story");

        // save action
        $("#story-form")
            .find(".save")
            .click(_.bind(function () {
               var data = {}
               var form_data = $("#story-form form").serializeArray();
               for (var i in form_data) {
                   data[form_data[i].name]=form_data[i].value;
               }
               this.model.set(data)
               this.model.save(data);
               $("#story-form").modal("hide");
           }, this))

    },
    initialize: function () {
        this.model.bind('change', this.render, this);
    },
    render: function () {
        $(this.el).data("model",this.model)
        $(this.el).html(this.template(this.model.toJSON())).addClass("story")
        return this;
    }
})

var StorySetView = Backbone.View.extend({
    tagName : "ul",
    initialize : function (model, options) {
        this.stage = options["stage"];
        this.model.bind("reset", this.render, this);
        this.model.bind("add", this.render, this);
        $(this.el).sortable({
            items: "li:not(.not-sortable)",
            connectWith: ".stage ul",
            stop : function (event, ui) {
                /* Swap stages */
                var swap_stage = $(ui.item).parent().data("stage");
                $(ui.item).data("model").set( {"stage":swap_stage})
                $(ui.item).data("model").save()

                /* Ordering items */
                var i = 1;
                $(this).find(".story").each(function () {
                    var model = $(this).data("model");
                    model.set({"order" : i++});
                    model.save()
                })
            }
        }).data("stage", this.stage);
    },
    events : {
        'click .new' : 'new_story'
    },
    new_story : function () {
        var story = new Story()
        story.set({
            "description": "",
            "color" : "yellow",
            "stage" : this.stage
        })
        story.save()
        this.model.add(story);
    },
    render : function () {
        $(this.el).empty()
        _.forEach(this.model.models, function (story) {
            $(this.el).append(  new StoryView({model:story}).render().el  )
        }, this)
        $(this.el).append("<li class='drop'>");        
        $(this.el).append($("<li style='' class='not-sortable'>").html($("<a class='new btn btn-large'>").html("New")))
        return this;
    }
})

var StageItemView = Backbone.View.extend({
    tagName : "div",
    template : _.template( $("#StageTemplate").html()),
    initialize : function () {
        this.model.bind('change', this.render, this)
        this.stories = new StorySet()
        this.storyset_view = new StorySetView({model:this.stories}, {stage:this.model})
        this.stories.fetch( {data:{stage : this.model.get("id")}} )
    },
    render : function () {
        $(this.el).addClass("stage")
            .html(this.template(this.model.toJSON()))
            .find(".stories").html( this.storyset_view.el )
        return this;
    }
})

var StageSetView = Backbone.View.extend({
    el : $("#stages"),
    initialize : function () {
        this.model.bind('reset', this.render, this)
    },
    render : function () {
        $(this.el).empty()
        _.each(this.model.models, function (stage) {
            $(this.el).append(new StageItemView({model:stage}).render().el)
        }, this)
        return this;
    }

})

var BoardItemView = Backbone.View.extend({
    tagName : "li" ,
    template : _.template($("#BoardItemTemplate").html()),
    events : {
        "click a" : "make_active"
    },
    initialize : function () {
        $(this.el).attr("id", "boarditem-" + this.model.get("id"))
    },
    make_active : function () {
        $(this.el).siblings().removeClass("active")
        $(this.el).addClass("active");
    },
    render : function () {
        $(this.el).html(this.template({"item" : this.model.toJSON()}))
        return this;
    }
})

var BoardSetView = Backbone.View.extend({
    el : $("#select-board"),
    events : {
        "click .new" : "new_board"
    },
    new_board : function () {
        var board_title = window.prompt("Title ?");
        if (!board_title) return;
        var board = new Board({"title" : board_title})
        board.save()
        this.model.fetch()
    },
    initialize : function () {
        this.boarditem_views = []
        this.model.bind("reset", this.render, this);
        this.model.bind("add", this.render, this);
    },
    render : function () {
        $(this.el).find("ul").empty();
        _.forEach(this.model.models, function (item) {
            $(this.el).find("ul").append((new BoardItemView({model:item})).render().el)
        }, this)
        return this;
    }
})


var BoardView = Backbone.View.extend({
    el : $("#board-info"),
    initialize : function () {
        this.model.bind("change", this.render, this);
    },
    events : {
        "click .destroy" : 'destroy',
        "click .change" : 'change'
    },
    change : function () {
        var title = window.prompt("Title ?", this.model.get("title"))
        if (!title) return;
        this.model.set("title", title)
        this.model.save({ success: this.render })
    },
    destroy : function () {
        if (window.confirm('Are you sure ?')) {
            this.model.destroy({
                success : function () {
                    window.location = '/';
                }
            });
        }

    },
    render : function () {
        $(this.el).find("#board-title").html(this.model.get("title"))
    }
})