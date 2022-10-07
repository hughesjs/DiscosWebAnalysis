from matplotlib import pyplot as plt


def plot_avg_csas_histo(discos_objects):
    avg_csas = [do["xSectAvg"] for do in discos_objects if
                "xSectAvg" in do.keys() and do["xSectAvg"] is not None and do["xSectAvg"] < 0.01]
    plt.hist(avg_csas, 10)
    plt.xlabel("Average CSA/m")
    plt.ylabel("Frequency")
    plt.title("Distribution of known debris of subcentimetre size from DISCOSweb")
    plt.show()


def setup_plotter():
    plt.style.use('dark_background')
