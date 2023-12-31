﻿namespace PeriodTracker
{
    internal class DataBaseManager : IDataBaseManager
    {
        private IDataBaseConnection _connection;
        private bool _isAppInDemoMode;

        public event EventHandler DemoModeChanged;

        public DataBaseManager()
        {
            _connection = new ProductionDataBaseConnection();
        }

        public IDataBaseConnection GetDataBaseConnection() => _connection;
        public bool IsAppInDemoMode() => _isAppInDemoMode;

        public async Task SetDemoDataBaseConnection()
        {
            await Task.Run(() =>
            {
                DisposeDataBaseConnection();
                _connection = new DemoDataBaseConnection();
                _isAppInDemoMode = true;
                NotifyDemoModeChanged();
            }
            );
        }

        public async Task SetProductionDataBaseConnection()
        {
            await Task.Run(() =>
            {
                DisposeDataBaseConnection();
                _connection = new ProductionDataBaseConnection();
                _isAppInDemoMode = false;
                NotifyDemoModeChanged();
            }
            );
        }

        private void NotifyDemoModeChanged()
        {
            DemoModeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DisposeDataBaseConnection()
        {
            if(_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
