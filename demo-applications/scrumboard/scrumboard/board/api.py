from django.http import HttpResponse
from tastypie.authorization import Authorization
from tastypie.resources import ModelResource
from tastypie import fields
from tastypie.constants import ALL_WITH_RELATIONS
from scrumboard.board.models import Story, Board, Stage

class BoardResource(ModelResource):
    def obj_create(self, bundle, request=None, **kwargs):
        kwargs["user_id"] = 1 # TODO: Authentication
        return super(BoardResource, self).obj_create(bundle, request, **kwargs)

    class Meta:
        queryset = Board.objects.all()
        #include_resource_uri = False
        authorization = Authorization()

class StageResource(ModelResource):
    board = fields.ForeignKey(BoardResource, 'board',full=True)
    class Meta:
        queryset = Stage.objects.all()
        #include_resource_uri = False
        authorization = Authorization()
        filtering = {
            "board" : ('exact', )
        }

class StoryResource(ModelResource):
    stage = fields.ForeignKey(BoardResource, 'stage',full=True)
    def obj_create(self, bundle, request=None, **kwargs):
        stage = Stage.objects.get(id=bundle.data.pop("stage")["id"])
        return super(StoryResource, self).obj_create(bundle, request, stage=stage)

    def obj_update(self, bundle, request=None, **kwargs):
        """
        Haddinden fazla pis. Heroku uzerinde cok degisilik olaylar donuyor.
        Simdilik boyle.
        """
        story = Story.objects.get(id=kwargs.get("pk"))
        story.description = bundle.data.get("description")
        story.color = bundle.data.get("color")
        story.stage_id = bundle.data.get("stage").get("id")
        story.order = bundle.data.get("order")
        story.save()
        #stage = Stage.objects.get(id=bundle.data.pop("stage")["id"])#
        #return super(StoryResource, self).obj_update(bundle, request)

    class Meta:
        queryset = Story.objects.all()
        #include_resource_uri = False
        authorization = Authorization()
        filtering = {
            "stage" : ('exact', )
        }