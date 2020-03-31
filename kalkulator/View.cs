using System.Windows.Controls;

namespace kalkulator
{
    class View
    {
        Result result_lb;
        Buttons buttons;
        public View(Grid buttons_grid, Grid label_grid)
        {
            result_lb = new Result(label_grid);
            buttons = new Buttons(buttons_grid, result_lb);
        }

    }
}
