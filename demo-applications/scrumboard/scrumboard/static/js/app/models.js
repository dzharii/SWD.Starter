var Board = Backbone.Model.extend({
    urlRoot : "/api/board/"
})
var BoardSet = Backbone.Collection.extend({
    url : "/api/board/",
    model : Board
})

var Story = Backbone.Model.extend({
    urlRoot : "/api/story/"

});
var StorySet = Backbone.Collection.extend({
    url : "/api/story/"
});

var Stage = Backbone.Model.extend()
var StageSet = Backbone.Collection.extend({
    url : "/api/stage/"
})