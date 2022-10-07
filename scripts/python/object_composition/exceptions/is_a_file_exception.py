class IsAFileException(Exception):
    def __init__(self, path: str):
        self.path = path
        self.message = f"{str} is a file, not a directory"
