from django.shortcuts import render_to_response
from django.template.context import RequestContext
from core.decorators import render_template


@render_template
def app(request):
    return "board/app.html"

@render_template
def home(request):
    return "index.html"