import os
from plotting import plotting
from analysis import discos_object
from utilities import collection_utils

def main():
    root_data_path: str = "../../../data"
    raw_data_path: str = os.path.join(root_data_path, "raw")
    latest_data_path: str = get_latest_data_dir(raw_data_path)

    plotting.setup_plotter()
    perform_analysis(latest_data_path)


def perform_analysis(latest_data_path: str) -> None:
    discos_object.analyse_discos_objects(latest_data_path)
    # analyse_fragmentation_events()
    # analyse_orbits()
    # analyse_reentries()
    # analyse_launches()
    # analyse_launch_vehicles_systems_and_propellant()
    # analyse_entities()

    # plotting.plot_avg_csas_histo(utilities.deserialise_discos_object_dump(os.path.join(latest_data_path, os.listdir(latest_data_path)[1])))
    pass


def get_latest_data_dir(raw_data_path: str) -> str:
    raw_path_content: list[str] = os.listdir(raw_data_path)
    dates: list[int] = [int(date) for date in raw_path_content if str.isnumeric(date)]
    greatest_date = max(dates)
    return os.path.join(raw_data_path, str(greatest_date))


if __name__ == '__main__':
    main()
