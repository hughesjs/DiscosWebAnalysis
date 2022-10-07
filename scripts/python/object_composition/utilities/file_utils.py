import json
import os.path
from typing import List
from utilities.collection_utils import first
from exceptions.is_a_file_exception import IsAFileException


def get_filepath_in_dir_with_name_containing(path: str, name_contains: str) -> str:
    if os.path.isfile(path):
        raise IsAFileException(path)

    files: list[str] = os.listdir(path)
    file_name: str | None = first(files, lambda x: name_contains in x)
    if file_name is None:
        print(f"File not found in {str}")

    return os.path.join(path, file_name)


def deserialise_discos_object_dump(file_path) -> List:
    print(file_path)
    with open(file_path) as f:
        lines = f.read()

    object_list = json.loads(lines)
    return object_list




