from typing import List
import json
import os.path


def deserialise_discos_object_dump(file_path) -> List:
    print(file_path)
    with open(file_path) as f:
        lines = f.read()

    object_list = json.loads(lines)
    return object_list
