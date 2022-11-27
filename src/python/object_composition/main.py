import json
import sys
from matplotlib import pyplot as plt


def deserialise_discos_object_dump(file_path):
    print(file_path)
    with open(file_path) as f:
        lines = f.read()

    object_list = json.loads(lines)
    return object_list


def plot_avg_csas_histo(discos_objects):
    avg_csas = [do["xSectAvg"] for do in discos_objects if
                "xSectAvg" in do.keys() and do["xSectAvg"] is not None and do["xSectAvg"] < 0.01]
    plt.hist(avg_csas, 10)
    plt.xlabel("Average CSA/m")
    plt.ylabel("Frequency")
    plt.title("Distribution of known debris of subcentimetre size from DISCOSweb")
    plt.show()


if __name__ == '__main__':
    discos_objects = deserialise_discos_object_dump(sys.argv[1])
    plt.style.use('dark_background')
    plot_avg_csas_histo(discos_objects)
