from django.contrib.auth.models import User
from django.db import models
from django.db.models.aggregates import Max, Count
from django.db.models.signals import post_save
from django.utils.encoding import smart_unicode

class Board(models.Model):
    user = models.ForeignKey(User)
    title = models.CharField(max_length=255)

    def __unicode__(self):
        return smart_unicode(self.title)

STAGE_CHOICES = (
    ('archive', 'Archive'),
    ('todo', 'Todo'),
    ('progress', 'Progress'),
    ('review', 'Review'),
    ('done', 'Done'),
)
class Stage(models.Model):
    board = models.ForeignKey(Board)
    order = models.IntegerField()
    title = models.CharField(max_length=30, choices=STAGE_CHOICES)

    class Meta:
        ordering = ["order", ]

    def __unicode__(self):
        return smart_unicode(self.title)


COLOR_CHOICES = (
    ('yellow', 'Yellow'),
    ('green', 'Green'),
    ('pink', 'Pink'),
    ('blue', 'Blue'),
    ('orange', 'Orange'),
)
class Story(models.Model):
    stage = models.ForeignKey(Stage)
    description = models.TextField()
    color = models.CharField(max_length=30, choices=COLOR_CHOICES)
    order = models.IntegerField(default=1)

    class Meta:
        ordering = ["order", ]

    def save(self, **kwargs):
        if self.pk and not self.order:
            # self.order = (self.stage.story_set.aggregate(Max("order")).get("order__max") or 0)  + 1
            # TODO: use aggregate for performance !
            from operator import attrgetter
            try:
                total = max(self.stage.story_set.all(), key=attrgetter("order")) + 1
            except ValueError:
                total = 1
            self.order = total
            self.save()
        super(Story, self).save(**kwargs)

    def __unicode__(self):
        return smart_unicode(self.description)

def create_stages(sender, **kwargs):
    if kwargs.get("created"):
        instance = kwargs["instance"]
        for order, stage in enumerate(STAGE_CHOICES):
            key, label = stage
            instance.stage_set.get_or_create(order=order, title = key)

post_save.connect(create_stages, sender=Board)