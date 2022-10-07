from utilities.collection_utils import first


def test_first_present():
    collection = [1, 2, 3, 4, 5, 6]
    result = first(collection, lambda x: x % 2 == 0)
    assert result == 2


def test_first_not_present():
    collection = [1, 2, 3, 4, 5, 6]
    result = first(collection, lambda x: x % 7 == 0)
    assert result is None
