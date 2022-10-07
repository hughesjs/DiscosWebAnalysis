from typing import List, TypeVar, Callable

T = TypeVar("T")
nT = T | None


def first(collection: List[T], predicate: Callable[[T], bool]) -> nT:
    for item in collection:
        if predicate(item):
            return item
    return None


