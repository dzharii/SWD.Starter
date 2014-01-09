from decorator import decorator
from django.http import HttpResponseRedirect
from django.shortcuts import render_to_response
from django.template.context import RequestContext

@decorator
def render_template(func, *args, **kwargs):
    """
    using example:
        @render_template
        def view(request, template='abc.html'):
            slot = "this is a slot"
            return template, {'slot' : slot}
    """
    request = args[0]
    _call = func(*args, **kwargs)

    if isinstance(_call, HttpResponseRedirect):
        return _call

    if isinstance(_call, tuple):
        template, context = _call
    else:
        template, context = _call, {}

    return render_to_response(template, context_instance=RequestContext(request, context))
