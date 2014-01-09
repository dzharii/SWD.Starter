from django.conf.urls.defaults import patterns, url, include
from tastypie.api import Api
from scrumboard.board.api import StoryResource, StageResource, BoardResource

api = Api(api_name='api')
api.register(StoryResource())
api.register(StageResource())
api.register(BoardResource())

urlpatterns = patterns('',
    url(r'^$', 'scrumboard.board.views.app', name='dashboard'),
    (r'^', include(api.urls)),
)
