from django.contrib import admin
from board.models import Board, Stage, Story


class StageInline(admin.TabularInline):
    model = Stage


class BoardAdmin(admin.ModelAdmin):
    inlines = [StageInline, ]

class StoryInline(admin.TabularInline):
    model = Story

class StageAdmin(admin.ModelAdmin):
    inlines = [StoryInline, ]

admin.site.register(Stage, StageAdmin)
admin.site.register(Board, BoardAdmin)
