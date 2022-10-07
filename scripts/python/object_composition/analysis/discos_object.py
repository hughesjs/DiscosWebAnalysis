from typing import Final
from utilities import file_utils

OBJECT_FILE_SLUG: Final[str] = "DiscosObject"


def analyse_discos_objects(data_dir_path: str) -> None:
    data_file_path: str = file_utils.get_filepath_in_dir_with_name_containing(data_dir_path, OBJECT_FILE_SLUG)
    data = file_utils.deserialise_discos_object_dump(data_file_path)

    pass
