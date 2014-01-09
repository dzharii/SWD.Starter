installation;

    $ virtualenv --no-site-packages scrum-env
    $ cd scrum-env
    $ source bin/activate
    $ git clone git://github.com/fatiherikli/scrumboard.git
    $ cd scrumboard
    $ pip install -r requirements.txt
    $ python scrumboard/manage.py runserver