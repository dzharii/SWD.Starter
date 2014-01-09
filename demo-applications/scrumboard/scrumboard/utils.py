import os
def here(x):
    return os.path.join(os.path.abspath(os.path.dirname(__file__)), x)
